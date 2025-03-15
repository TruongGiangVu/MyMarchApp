// import pino from 'pino';
// import path from 'path';
// // eslint-disable-next-line @typescript-eslint/no-require-imports
// const pinoRotate = require('pino-daily-rotate-file');

// const logDirectory = path.join(process.cwd(), 'logs');

// const transport = pinoRotate({
//     filename: path.join(logDirectory, 'app-%DATE%.log'),
//     datePattern: 'YYYY-MM-DD',
//     zippedArchive: true,
//     maxSize: '10m'
// });

// const logger = pino(
//     {
//         level: 'info',
//         formatters: {
//             bindings: (bindings) => ({ pid: bindings.pid }),
//             level: (label) => ({ level: label }),
//         },
//     },
//     transport
// );

// export default logger;

export const LOG_TEMP = '';