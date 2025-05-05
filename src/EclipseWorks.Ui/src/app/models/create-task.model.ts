import { TaskPriority } from "./task.model";

export interface CreateTask {
    projectId: Number,
    title: string,
    description: string,
    priority: TaskPriority,
    duedate: Date
}