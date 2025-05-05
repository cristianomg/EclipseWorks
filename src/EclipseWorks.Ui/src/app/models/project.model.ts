import { User } from "./user.model";

export interface Project {
    id: number,
    name: string,
    userId: number,
    user: User,
    tasks: [],
    createdAt: Date,
    updatedAt: Date | null
}