from firebase_admin import credentials, firestore, storage
import pyrebase
import firebase_admin


# Initialize Firebase
def init_firebase():
    cred = credentials.Certificate('credentials.json')
    # Include your storage bucket name below
    return firebase_admin.initialize_app(cred, {
        'storageBucket': 'alarmscreenshot.appspot.com'
    })
firebase_app = init_firebase()

config={
  "apiKey": "",
  "authDomain": "",
  "databaseURL": "",
  "projectId": "",
  "storageBucket": "",
  "messagingSenderId": "",
  "appId": "",
  "measurementId": ""
}


firebase=pyrebase.initialize_app(config)
auth=firebase.auth()
#Create User
email="securevisionai@gmail.com"
password=""
#auth.create_user_with_email_and_password(email,password)
#Sign in
user=auth.sign_in_with_email_and_password(email,password)


def storeImageAndGetURL(image_path,track_id):
    file_name="Alert_"+str(track_id)
    bucket = storage.bucket()
    blob = bucket.blob(file_name)
    image=blob.upload_from_filename(image_path)
    # Get url of image
    url = firebase.storage().child(file_name).get_url(user['idToken'])
    return url





