from os import environ
is_docker = environ.get("IS_DOCKER", "false") == "true"
if (is_docker):
    print("As of now, this script does not work while running inside of docker. Please run it locally.")
    exit(-1)

print("Listening for data on the serial bus.");
