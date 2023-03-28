**注意canal的docker镜像只能在centos下运行**

1. 将项目中192.168.31.43改为自己的ip地址

2. 进入canal容器里面，更改conf/canal.properties配置

   ```properties
   canal.serverMode = rabbitMQ
   rabbitmq.host = 127.0.0.1
   rabbitmq.virtual.host = / 
   rabbitmq.exchange = xxx
   rabbitmq.username = xxx
   rabbitmq.password = xxx
   rabbitmq.deliveryMode = fanout
   ```
