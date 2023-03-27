进入canal容器里面，更改conf/canal.properties配置

canal.serverMode = rabbitMQ
rabbitmq.host = 127.0.0.1
rabbitmq.virtual.host = / 
rabbitmq.exchange = xxx
rabbitmq.username = xxx
rabbitmq.password = xxx
rabbitmq.deliveryMode = fanout # 请根据不同场景选择类型