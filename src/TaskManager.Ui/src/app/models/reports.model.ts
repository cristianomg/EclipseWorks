export interface CompletedTasksCountByUserLast30Days {
    completedTasksPerUser: {userName: string, count: Number}[]
    average: Number
}