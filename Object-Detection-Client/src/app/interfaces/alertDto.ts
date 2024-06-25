export interface AlertDto{
    id: string;
    eventType: string;
    capturedTime: Date;
    location: string;
    confidence: string;
    safezone: number;
    actionStatus: string;
}