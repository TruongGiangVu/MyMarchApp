import { AuditEntity } from "..";

export interface ToDoTag extends AuditEntity {
    id: string;
    text: string;
    color: string;
    toDoItems?: string[] | null;
}