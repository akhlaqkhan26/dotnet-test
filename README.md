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

- On Linux:
mkdir auth
docker run \
  --entrypoint htpasswd \
  httpd:2 -Bbn testuser testpassword > auth/htpasswd

- On Windows
docker run --rm --entrypoint htpasswd httpd:2 -Bbn testuser testpassword | Set-Content -Encoding ASCII auth/htpasswd


docker run -d -p 5000:5000 \
-v $HOME/watchtower/auth:/auth \
-e "REGISTRY_AUTH=htpasswd" \
-e "REGISTRY_AUTH_HTPASSWD_REALM=Registry Realm" \
-e REGISTRY_AUTH_HTPASSWD_PATH=/auth/htpasswd \
--restart always --name registry registry:2.7

6. Setup Wacthtower

- create credentials config json
    docker login registry:5000
- Your password will be stored unencrypted in $HOME/.docker/config.json.
- Or Create manually config.json with formart:
{
    "auths": {
            "registry:5000": {
                "auth": "s3cr3t"
            }
        }
}

- run watchtower
docker run -d \
--name watchtower \
--add-host=registry:host-gateway \
-v $HOME/watchtower/config.json:/config.json \
-v /var/run/docker.sock:/var/run/docker.sock containrrr/watchtower:1.6.0 --interval 10 dotnet-test --debug


6. Run service as container

 docker run -d --name dotnet-test -p 8080:80 registry:5000/dotnet-test:latest

7. create changes and push repository