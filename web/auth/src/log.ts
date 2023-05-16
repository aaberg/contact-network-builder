import winston from "winston";

export const log = winston.createLogger({
    level: "debug",
    format: winston.format.json(),
    defaultMeta: {service: 'contact-network-web'},
    transports: [
        new winston.transports.Console({
            format: winston.format.simple()
        })
    ]
})