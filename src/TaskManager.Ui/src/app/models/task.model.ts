import { Project } from "./project.model";
import { TaskComment } from "./task-comment.model";

export interface Task {
    id: number,
    title: string,
    description: string,
    projectId: number,
    dueDate: Date,
    priority: TaskPriority,
    status: TaskStatus,
    project: Project | null,
    histories: any[],
    comments: TaskComment[]
    createdAt: Date,
    updatedAt: Date,
    delayed: boolean
}


export enum TaskStatus {
    Pending = 1,
    InProgress = 2,
    Completed = 3,
}

export enum TaskPriority {
    Low = 1,
    Medium = 2,
    High = 3,
}