def arrangeSafeZone(eventType):
    switcher={
        "gun":1,
        "fire":2,
        "knife":3,
        "smoke":4
    }
    return switcher.get(eventType,5)
#5=no safezone
