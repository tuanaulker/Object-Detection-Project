export interface Log{
    id: string;
    eventType: string;
    capturedTime: Date;
    capturedImage: string;
    location: string;
    confidence: string;
    safezone: number;
    userId: string;
    areas: string;
    actionTaken: string;
    actionTime: Date;
    details: string;
    actionStatus: string;
    status: boolean;
}