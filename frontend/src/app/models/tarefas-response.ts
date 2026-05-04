import { Tarefa } from "./tarefa";

export interface TarefasResponse {
    todo: Tarefa[];
    inProgress: Tarefa[];
    done: Tarefa[];
}
