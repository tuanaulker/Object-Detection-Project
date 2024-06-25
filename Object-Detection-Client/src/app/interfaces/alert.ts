export interface Alert{
    id: string,
    eventType: string,
    capturedTime: Date;
    capturedImage: string;
    location: string;
    confidence: string;
    safezone: number;
    actionStatus: string;
}