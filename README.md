

docker run \      
    --volume=drone-server-data:/data \
    --env=DRONE_GITHUB_CLIENT_ID=fe03a856a435eab802cb \
    --env=DRONE_GITHUB_CLIENT_SECRET=93dc10a71dd572b437910693ae2b5c04949c39a8 \
    --env=DRONE_RPC_SECRET=b014154316bfe1de52559ad3dd306386 \
    --env=DRONE_SERVER_HOST=b306-111-68-25-162.ngrok-free.app  \
    --env=DRONE_SERVER_PROTO=https \
    --env=DRONE_REGISTRY_PLUGIN_ENDPOINT=http://localhost:5000 \
    --env=DRONE_USER_CREATE=username:akhlaqkhan26,admin:true \
    --env=DRONE_REGISTRY_PLUGIN_SKIP_VERIFY=true \
    --publish=9000:80 \
    --publish=9443:443 \
    --restart=always \
    --detach=true \
    --name=drone \
    drone/drone:2

docker run --detach \
    --volume=/var/run/docker.sock:/var/run/docker.sock \
    --env=DRONE_RPC_PROTO=https \
    --env=DRONE_RPC_HOST=b306-111-68-25-162.ngrok-free.app  \
    --env=DRONE_RPC_SECRET=b014154316bfe1de52559ad3dd306386 \
    --env=DRONE_RUNNER_CAPACITY=2 \
    --env=DRONE_RUNNER_NAME=my-first-runner \
    --publish=3000:3000 \
    --restart=always \
    --name=runner \
    drone/drone-runner-docker:1


docker run --detach \
    --env=DRONE_RPC_PROTO=https \
    --env=DRONE_RPC_HOST=b306-111-68-25-162.ngrok-free.app  \
    --env=DRONE_RPC_SECRET=b014154316bfe1de52559ad3dd306386 \
    --publish=3000:3000 \
    --restart always \
    --name runner \
    drone/drone-runner-ssh



DRONE_RPC_PROTO=https
DRONE_RPC_HOST=fd2a-111-68-25-162.ngrok-free.app
DRONE_RPC_SECRET=814d768a8dbd8f1cb1f3a32cab49aed4




rty


