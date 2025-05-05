export interface User {
    name: string,
    role: UserRole,
    id: number,
    createdAt: Date,
    updatedAt: Date | null
}


export enum UserRole {
    User= 1,
    Manager = 2
}