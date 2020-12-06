
const proxy = require('http-proxy-middleware');

module.exports = (app) => {
    app.use(proxy('/api', {
        target: 'http://localhost:6528/',
        changeOrigin: true,
        logLevel: "debug"
    }));
};
