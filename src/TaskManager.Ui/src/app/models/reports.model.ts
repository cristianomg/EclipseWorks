export interface CompletedTasksCountByUserLast30Days {
    completedTasksPerUser: {userName: string, count: Number}[]
    average: Number
}

export interface DelayedTasksByUsers {
    delayedTasksByUsers : {userName: string, count: Number}[]
    average: Number
}