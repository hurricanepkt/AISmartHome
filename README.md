# AIS Smart Home - AISmartHome

Decodes AIS packets then gives home assistant a nice rest API to interact with.

## Installation

Using docker compose 

```docker-compose.yml
    AISmarthome:
        container_name : AISmarthome
        image: markgreenway/aismarthome:latest
        ports:
            - 8082:80
            - "10111:10110/udp"
        restart: always
        volumes:
            - /AppData/aismarthome:/Data
```
## Source Code

[github.com/hurricanepkt/AISmartHome](https://github.com/hurricanepkt/AISmartHome)

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://github.com/hurricanepkt/AISmartHome/blob/master/LICENSE)