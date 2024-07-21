from os import environ, path
from serial import Serial
from regex import Regex
from datetime import datetime, UTC
from io import open
import csv

# Settings
serialBusName = "/dev/cu.usbmodem1101"
baudRate = 9600
exportDestination = "data"

# Exit when running in the docker container
is_docker = environ.get("IS_DOCKER", "false") == "true"
if (is_docker):
    print("As of now, this script does not work while running inside of docker. Please run it locally.")
    exit(-1)

# Begin data collection
dataPattern = Regex(rb"dutyCycle:(\d+),rpm:(\d+)\s*")

serialBus = Serial(serialBusName, baudRate)
print("Serial bus opened. Sending the start signal");


data = []
while True:
    currentLine = serialBus.readline().strip()

    if currentLine == b"clear":
        print("Registered the start of a new data set.")
        data.clear()
        continue

    if currentLine == b"done":
        break

    matches = dataPattern.match(currentLine)
    if matches is None:
        print(f"Ignoring an invalid line: {currentLine}")
        continue
    
    dutyCycle = int(matches.group(1))
    rpm = int(matches.group(2))
    data.append((dutyCycle, rpm))

    print(f"dutyCycle: {dutyCycle}\trpm: {rpm}")
serialBus.close()

print(f"Measurement is done. {len(data)} records have been collected.")

data.insert(0, ("dutyCycle", "rpm"))

fileName = "measurement-" + datetime.now(UTC).strftime("%Y-%m-%d") + ".csv"
with open(path.join(exportDestination, fileName), "w") as fileObject:
    writer = csv.writer(fileObject)
    writer.writerows(data)

print("Done.");
