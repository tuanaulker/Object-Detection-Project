import psycopg2
import psycopg2.extras

#database informations
host = ""
dbname = ""
user = ""
password = ""
port = ""
conn=None
cur=None


def insert_alert_info(alert):
    try:
        conn = psycopg2.connect(
            host=host,
            dbname=dbname,
            user=user,
            password=password,
            port=port
        )
        # Register UUID adapter
        psycopg2.extras.register_uuid(conn_or_curs=conn)
        cur = conn.cursor()
        insert_script = ('INSERT INTO logs (id, eventtype, capturedtime, capturedimage, location, status, safezone, confidence, actionstatus,ispublished, areas) '
                         'VALUES(%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)')
        insert_value = (alert.id, alert.EventType, alert.CapturedTime, alert.CapturedImage, alert.Location, alert.Status, alert.safeZone, alert.confidence, alert.actionStatus,alert.isPublished, alert.area)
        cur.execute(insert_script, insert_value)
        conn.commit()

    except Exception as error:
        print("An error occurred:", error)

    finally:
        if cur is not None:
            cur.close()
        if conn is not None:
            conn.close()


