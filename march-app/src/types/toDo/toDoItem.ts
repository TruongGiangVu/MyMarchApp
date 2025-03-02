import { ToDoCheckList, ToDoTag } from ".";
import { AuditEntity } from "..";

export interface ToDoItem extends AuditEntity {
    id: string;
    name: string;
    rate?: number | null;
    description?: string | null;
    isDone: boolean;
    priority: string;
    toDoCheckLists?: ToDoCheckList[] | null;
    toDoTags?: ToDoTag[] | null;
}