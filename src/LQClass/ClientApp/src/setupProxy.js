
const proxy = require('http-proxy-middleware');

module.exports = (app) => {
    app.use(proxy('/api', {
        target: 'http://localhost:7126/',
        changeOrigin: true,
        logLevel: "debug"
    }));
};
