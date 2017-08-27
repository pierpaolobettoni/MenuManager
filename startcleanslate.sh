# Delete all containers
docker rm $(docker ps -a -q)
# Delete all images
docker rmi $(docker images -q)
# build
docker-compose build
# run
docker-compose up
