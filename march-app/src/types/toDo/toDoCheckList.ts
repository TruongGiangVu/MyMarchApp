import { AuditEntity } from "..";

export interface ToDoCheckList extends AuditEntity {
    id: string;
    text: string;
    isDone: boolean;
    toDoItemId?: string;
}