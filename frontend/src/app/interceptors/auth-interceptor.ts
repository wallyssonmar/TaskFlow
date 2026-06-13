import { HttpErrorResponse, HttpEvent, HttpInterceptorFn } from '@angular/common/http';
import { AuthService } from '../services/auth-service';
import { inject, PLATFORM_ID } from '@angular/core';
import { catchError, Observable, switchMap, throwError } from 'rxjs';
import { isPlatformBrowser } from '@angular/common';
let isRefreshing = false;
let refreshQueue: Array<(token: string) => void> = [];

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const platformId = inject(PLATFORM_ID);
  if (!isPlatformBrowser(platformId)) {
    return next(req);
  }

  const authService = inject(AuthService);
  const token = localStorage.getItem('token');

  const authReq = token
    ? req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`,
        },
      })
    : req;

  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status !== 401) {
        return throwError(() => error);
      }

      const refreshToken = localStorage.getItem('refreshToken');
      if (!refreshToken) {
        return throwError(() => error);
      }

      if (isRefreshing) {
        return new Observable<HttpEvent<unknown>>((observer) => {
          refreshQueue.push((newToken: string) => {
            const retryReq = req.clone({
              setHeaders: {
                Authorization: `Bearer ${newToken}`,
              },
            });
            next(retryReq).subscribe({
              next: (res) => observer.next(res),
              error: (err) => observer.error(err),
              complete: () => observer.complete(),
            });
          });
        });
      }
      isRefreshing = true;

      return authService.refreshToken(refreshToken).pipe(
        switchMap((response) => {
          isRefreshing = false;

          localStorage.setItem('token', response.token);
          localStorage.setItem('refreshToken', response.refreshToken);

          refreshQueue.forEach((cb) => cb(response.token));
          refreshQueue = [];

          const retryReq = req.clone({
            setHeaders: {
              Authorization: `Bearer ${response.token}`,
            },
          });
          return next(retryReq);
        }),
        catchError((err) => {
          isRefreshing = false;
          refreshQueue = [];
          return throwError(() => err);
        }),
      );
    }),
  );
};
