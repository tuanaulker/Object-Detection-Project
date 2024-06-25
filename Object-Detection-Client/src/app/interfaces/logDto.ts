export interface LogDto{
    id: string;
    eventType: string;
    capturedTime: Date;
    capturedImage: string;
    location: string;
    confidence: string;
    safezone: number;
    areas: string;
    actionStatus: string;
}