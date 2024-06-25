import uuid


class Alert:
    def __init__(self,EventType,CapturedTime,CapturedImage,Location,safeZone,confidence, area):
        self.id=uuid.uuid4()
        self.EventType=EventType
        self.CapturedTime=CapturedTime
        self.CapturedImage=CapturedImage
        self.Location=Location
        self.Status=True
        self.safeZone=safeZone
        self.confidence=confidence
        self.actionStatus='Alert'
        self.isPublished=False
        self.area=area
    def __str__(self):
        return f"{self.id}-{self.EventType}-{self.CapturedTime}-{self.CapturedImage}-{self.Location}-{self.Status}-{self.safeZone}-{self.confidence}"