# dotnetnats-101

Sample projects and small code using .NET (C#) client library.

## JetStream Reconnection

> Note: Create file stream `foo` subscribed to `foo.*` and durable pull consumer `foocon1`

### JetStream publishing (w/ACK) surviving a server disconnect event

```bash
todd@mort:~/lab/dotnetnats-101/nats-js-pub/bin/Debug$ ./nats_js_pub.exe -url "nats://localhost:4222" -creds "/home/todd/lab/nats-local-dauth/vault/.nkeys/creds/NatsOp/AcctA/UserA1.creds" -subject foo.a -count 1000 -payload "bubba"

JetStream Publish Example
  Url: nats://localhost:4222
  Subject: foo.a
  Payload: bubba
  Creds: **********
  Count: 1000
  Delay: 1

Published message 'bubba-1' on subject 'foo.a', stream 'foo', seqno '26'.
Published message 'bubba-2' on subject 'foo.a', stream 'foo', seqno '27'.
Published message 'bubba-3' on subject 'foo.a', stream 'foo', seqno '28'.
Published message 'bubba-4' on subject 'foo.a', stream 'foo', seqno '29'.
Published message 'bubba-5' on subject 'foo.a', stream 'foo', seqno '30'.
DisconnectedEvent, Connection: 11
Unable to publish message
Unable to publish message
Unable to publish message
ReconnectedEvent, Connection: 11
Published message 'bubba-9' on subject 'foo.a', stream 'foo', seqno '34'.
Published message 'bubba-10' on subject 'foo.a', stream 'foo', seqno '35'.
Published message 'bubba-11' on subject 'foo.a', stream 'foo', seqno '36'.
```

### JetStream Pull Subscription surviving a server disconnect event

```bash
todd@mort:~/lab/dotnetnats-101/nats-js-subdds/bin/Debug$ ./nats_js_subdds.exe -url "nats://localhost:4222" -creds "/home/todd/lab/nats-local-dauth/vault/.nkeys/creds/NatsOp/AcctA/UserA1.creds"

Pull Subscription using primitive Expires In Example
  Url: nats://localhost:4222
  Stream: foo
  Durable: foocon1
  Creds: **********
  Count: 15

1. Message: {Subject=foo.a;Reply=$JS.ACK.foo.foocon1.1.8.8.1669842288218628843.0;Payload=<bubba4>}
DisconnectedEvent, Connection: 27
ReconnectedEvent, Connection: 11
2. Message: {Subject=foo.a;Reply=$JS.ACK.foo.foocon1.1.9.9.1669842308791978931.0;Payload=<bubba5>}
3. Message: {Subject=foo.a;Reply=$JS.ACK.foo.foocon1.1.10.10.1669842923507376266.0;Payload=<bubba-1>}
4. Message: {Subject=foo.a;Reply=$JS.ACK.foo.foocon1.1.11.11.1669842923511188499.0;Payload=<bubba-2>}
5. Message: {Subject=foo.a;Reply=$JS.ACK.foo.foocon1.1.12.12.1669842923511494184.0;Payload=<bubba-3>}
6. Message: {Subject=foo.a;Reply=$JS.ACK.foo.foocon1.1.13.13.1669842923511713504.0;Payload=<bubba-4>}
7. Message: {Subject=foo.a;Reply=$JS.ACK.foo.foocon1.1.14.14.1669842923511973673.0;Payload=<bubba-5>}
8. Message: {Subject=foo.a;Reply=$JS.ACK.foo.foocon1.1.15.15.1669846801828813182.0;Payload=<bubba-1>}
9. Message: {Subject=foo.a;Reply=$JS.ACK.foo.foocon1.1.16.16.1669846802837231996.0;Payload=<bubba-2>}
10. Message: {Subject=foo.a;Reply=$JS.ACK.foo.foocon1.1.17.17.1669846803838663563.0;Payload=<bubba-3>}
11. Message: {Subject=foo.a;Reply=$JS.ACK.foo.foocon1.1.18.18.1669846804839987335.0;Payload=<bubba-4>}
12. Message: {Subject=foo.a;Reply=$JS.ACK.foo.foocon1.1.19.19.1669846805841975014.0;Payload=<bubba-5>}
13. Message: {Subject=foo.a;Reply=$JS.ACK.foo.foocon1.1.20.20.1669846820385180234.0;Payload=<bubba-1>}
14. Message: {Subject=foo.a;Reply=$JS.ACK.foo.foocon1.1.21.21.1669846821393272561.0;Payload=<bubba-2>}
15. Message: {Subject=foo.a;Reply=$JS.ACK.foo.foocon1.1.22.22.1669846822395076420.0;Payload=<bubba-3>}
DisconnectedEvent, Connection: 11
ClosedEvent, Connection: 11
```


