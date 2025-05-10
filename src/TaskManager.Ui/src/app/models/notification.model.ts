import { User } from "./user.model";

export interface NotificationEntity{
    value: string,
    userId: Number,
    read: boolean,
    readAt: Date,
    user: User,
    id: Number,
    createdAt: Date,
}