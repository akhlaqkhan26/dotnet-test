host =  https://fddf-111-68-25-162.ngrok-free.app

1. Create Network

docker network create devops

2. Setup Drone
```
docker run \     
    --network devops 
    --volume=drone-server-data:/data \
    --env=DRONE_GITHUB_CLIENT_ID=d29daf825fbf3a016c99 \
    --env=DRONE_GITHUB_CLIENT_SECRET=3009aefb53162ec72a2d65a4670b627e19269083 \
    --env=DRONE_RPC_SECRET=b014154316bfe1de52559ad3dd306386 \
    --env=DRONE_SERVER_HOST=fddf-111-68-25-162.ngrok-free.app  \
    --env=DRONE_SERVER_PROTO=https \
    --env=DRONE_USER_CREATE=username:akhlaqkhan26,admin:true \
    --publish=9000:80 \
    --publish=9443:443 \
    --restart=always \
    --detach=true \
    --name=drone \
    drone/drone:2
```

3. Setup Runner
docker run --detach \
  --network devops \
  --volume=/var/run/docker.sock:/var/run/docker.sock \
  --env=DRONE_RPC_PROTO=https \
  --env=DRONE_RPC_HOST=fddf-111-68-25-162.ngrok-free.app \
  --env=DRONE_RPC_SECRET=b014154316bfe1de52559ad3dd306386 \
  --env=DRONE_RUNNER_CAPACITY=2 \
  --env=DRONE_RUNNER_NAME=runner \
  --publish=3000:3000 \
  --restart=always \
  --name=runner \
  drone/drone-runner-docker:1

Config Runner if using exec runner:

DRONE_RPC_PROTO=http
DRONE_RPC_HOST=localhost:9000
DRONE_RPC_SECRET=b014154316bfe1de52559ad3dd306386
DRONE_LOG_FILE=/var/log/drone-runner-exec/log.txt


5. Setup Docker Registry

docker run -d --network devops --restart always --name registry registry:2.7

6. Setup Wacthtower

docker run -d \
--name watchtower \
--network devops -v /var/run/docker.sock:/var/run/docker.sock containrrr/watchtower:1.6.0 --interval 30 --monitor-only dotnet-test --debug


6. Run dotnet-test as container

docker run -d --name dotnet-test dotnet-test:1.0.7