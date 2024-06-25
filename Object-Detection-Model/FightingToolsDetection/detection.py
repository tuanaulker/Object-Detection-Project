from ultralytics import YOLO
import cv2
import cvzone
import math
from AddDataToDatabase import storeImageAndGetURL
from datetime import datetime, timezone
from Alert import Alert
from SafeZone import arrangeSafeZone
from ConnectPostgreSQL import insert_alert_info
import random
from AlertEmail import send_email

cap = cv2.VideoCapture('gun2.mp4')
if(cap.isOpened()=='False'):
    print("Error Reading the Video")


model = YOLO('best2.pt')
classNames = ['fire', 'gun', 'knife', 'smoke']

center_point=[]
count=0
center_point_previous_frame=[]
tracking_objects={}
track_id=0
location='Camera '+str(random.randint(1, 10))
fabrica_areas=['Manufacturing Area','Storage Area', 'Warehouse', 'Lab','Maintenance Workshop', 'Offices', 'Gardens' ]
area= random.choice(fabrica_areas)

while True:
    #Initialize frame counter
   count+=1
   center_point_current_frame=[]

   success, img = cap.read()
   if success:
       img = cv2.resize(img, (0, 0), fx=0.6, fy=0.6, interpolation=cv2.INTER_AREA)
       results = model(img, stream=True)


       for r in results:
           boxes = r.boxes

           for box in boxes:
               # Bounding Box
               x1, y1, x2, y2 = box.xyxy[0]
               x1, y1, x2, y2 = int(x1), int(y1), int(x2), int(y2)
               w, h = x2 - x1, y2 - y1  #Center Points
               cx = int((x2 + x1) / 2)
               cy = int((y1 + y2) / 2)
               center_point_current_frame.append((cx, cy))
               #Draw rectangle
               cv2.rectangle(img, (x1, y1), (x2, y2), (0,0,98), 2)
               conf = math.ceil((box.conf[0] * 100)) / 100
               cls = int(box.cls[0])
               #Write a text
               cvzone.putTextRect(img, f'{classNames[cls]} {conf * 100:.0f}%', (max(0, x1), max(35, y1)),
                                  scale=2, thickness=2, colorB=(0, 0, 0), colorT=(255, 255, 255))


       #First 2 frames
       if count <= 2:
           for pt in center_point_current_frame:
               for pt2 in center_point_previous_frame:
                   distance = math.hypot(pt2[0] - pt[0], pt2[1] - pt[1])
                   if distance < 20:
                       tracking_objects[track_id] = pt
                       track_id += 1
                       time = datetime.now(timezone.utc)
                       print(time)
                       cv2.imwrite("ToolsDetection/ToolDetection_" + str(track_id) + ".jpg", img)
                       screenshot_path = "ToolsDetection/ToolDetection_" + str(track_id) + ".jpg"
                       url = storeImageAndGetURL(screenshot_path, track_id)
                       results = model(img, stream=True)
                       for r in results:
                           boxes = r.boxes
                           for box in boxes:
                               confidence = math.ceil((box.conf[0] * 100))
                               cls = int(box.cls[0])
                               event = classNames[cls]
                               safeZone = arrangeSafeZone(classNames[cls])
                               if (safeZone != 5):
                                   alert = Alert(classNames[cls].capitalize(), time, url, location, safeZone,
                                                 confidence, area)
                                   insert_alert_info(alert)
                                   send_email(url, classNames[cls], location, time, alert.id,
                                              ['securevisionai@gmail.com'])
                                   print(url)
                                   print(alert)
        #Remaining frames
       else:
           tracking_objects_copy = tracking_objects.copy()
           center_point_current_frame_copy = center_point_current_frame.copy()
           for object_id, pt2 in tracking_objects_copy.items():
               object_exists = False
               for pt in center_point_current_frame_copy:
                   distance = math.hypot(pt2[0] - pt[0], pt2[1] - pt[1])
                   #If distance is smaller than 100, no new object
                   if distance < 100:
                       tracking_objects[object_id] = pt
                       object_exists = True
                       if pt in center_point_current_frame:
                           center_point_current_frame.remove(pt)
                       continue
               #If false, detect new object
               if not object_exists:
                   tracking_objects.pop(object_id)
           previous_track_id=track_id
           # Add new ID found
           for pt in center_point_current_frame:
               tracking_objects[track_id] = pt
               track_id += 1
               time = datetime.now(timezone.utc)
               #Take screenshot
               cv2.imwrite("ToolsDetection/ToolDetection_" + str(track_id) + ".jpg", img)
               screenshot_path = "ToolsDetection/ToolDetection_" + str(track_id) + ".jpg"
               url=storeImageAndGetURL(screenshot_path, track_id)
               results = model(img, stream=True)
               for r in results:
                   boxes = r.boxes
                   for box in boxes:
                       confidence = math.ceil((box.conf[0] * 100))
                       cls = int(box.cls[0])
                       event = classNames[cls]
                       safeZone = arrangeSafeZone(classNames[cls])
                       if (safeZone != 5):
                           alert = Alert(classNames[cls].capitalize(), time, url, location, safeZone,
                                         confidence, area)
                           insert_alert_info(alert)
                           send_email(url, classNames[cls], location, time, alert.id,
                                      ['securevisionai@gmail.com'])
                           print(url)
                           print(alert)
        #Write an object Id
       for object_id, pt in tracking_objects.items():
           cv2.circle(img, pt, 5, (0, 0, 255), -1)
           cv2.putText(img, str(object_id), (pt[0], pt[1] - 7), 0, 1, (255, 0, 0), 3)

       center_point_previous_frame = center_point_current_frame.copy()
       cv2.imshow('Image', img)
       if cv2.waitKey(1) & 0XFF == ord('1'):
           break

   else:
       break

cap.release()
cv2.destroyAllWindows()