from email.mime.image import MIMEImage
import requests
from email.mime.multipart import MIMEMultipart
import psycopg2.extras

#database informations
host = ""
dbname = ""
user = ""
password = ""
port = ""
conn=None
cur=None

try:
    conn = psycopg2.connect(
        host=host,
        dbname=dbname,
        user=user,
        password=password,
        port=port
    )
    psycopg2.extras.register_uuid(conn_or_curs=conn)
    cur = conn.cursor()
    cur.execute('SELECT email FROM ASPNETUSERS')
    emails = cur.fetchall()
    email_list = [email[0] for email in emails]

except Exception as error:
    print("An error occurred:", error)

finally:
    if cur is not None:
        cur.close()
    if conn is not None:
        conn.close()

email_list.append('securevisionai@gmail.com')


import smtplib
from email.mime.text import MIMEText

subject = "Urgent Security Alert: Potential Threat Detected"
body = "This is the body of the text message"
sender = "securevisionai@gmail.com"

password = ""

def send_email(url, eventType, location, time, id, recipients):
    sender = "securevisionai@gmail.com"
    password = ""
    msgRoot = MIMEMultipart('related')
    msgRoot['Subject'] = 'Urgent Security Alert: Potential Threat Detected'
    msgRoot['From'] = sender
    msgRoot['To'] = ', '.join(recipients)
    msgRoot.preamble = 'This is a multi-part message in MIME format.'

    msgAlternative = MIMEMultipart('alternative')
    msgRoot.attach(msgAlternative)

    msgText = MIMEText('This is the alternative plain text message.')
    msgAlternative.attach(msgText)
    new_hour = time.hour+3
    if new_hour>23:
        new_hour-=24
    new_time = time.replace(hour=new_hour)
    formatted_time = new_time.strftime("%H:%M:%S %d:%m:%Y")
    msgText = MIMEText(
        f'<b style="font-size:16px; color:black;">We regret to inform you that our security monitoring system has identified an incident that requires your immediate attention. '
        f'Detailed information is provided below:</b><br>'
        f'<b style="color:red; font-size:20px; margin-left:500px;">{str(eventType).upper()} WAS DETECTED ON {location.upper()}</b><br>'
        f'<div style="margin-top:20px; margin-bottom:20px;"><img src="cid:image1" style="width:50%; display:block; margin-left:auto; margin-right:auto;"></div>'
        f'<div style="color:red; font-size:18px; margin-top:10px; margin-bottom:20px; margin-left:600px;">TIME: {formatted_time}</div>'
        f'<a href="http://localhost:4200/log-detail/{str(id)}"; font-size:30px; color:blue;>For further process, Click!!</a>'
        '<div font-size:15px;>Stay Safe,<br></div>'
        '<div font-size:15px;>SecureVision AI</div>', 'html')
    msgAlternative.attach(msgText)

    response = requests.get(url)
    image_data = response.content

    msgImage = MIMEImage(image_data)
    msgImage.add_header('Content-ID', '<image1>')
    msgRoot.attach(msgImage)


    with smtplib.SMTP_SSL('smtp.gmail.com', 465) as smtp_server:
        smtp_server.login(sender, password)
        smtp_server.sendmail(sender, recipients, msgRoot.as_string())

    print("Message sent!")



