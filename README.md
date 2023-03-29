**注意canal的docker镜像只能在centos下运行**

1. 将项目和docker-compose中192.168.31.43改为自己的ip地址

2. 进入canal容器里面，更改conf/canal.properties配置

   ```properties
   canal.serverMode = rabbitMQ
   rabbitmq.host = 192.168.31.43
   rabbitmq.virtual.host = / 
   rabbitmq.exchange = canal
   rabbitmq.username = guest
   rabbitmq.password = guest
   rabbitmq.deliveryMode = fanout
   ```
