export interface Projeto {
    name: string;
    descricao: string;
    qtdTarefa: number;
    qtdTarefaConcluida: number;
    progresso?: number;
    qtdMembros: number;
    selectedColor: string;
}
