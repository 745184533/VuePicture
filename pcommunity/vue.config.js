module.exports = {
  publicPath: './',

  devServer: {
    port: 8055, // 端口
    open:true,
    // proxy:{
    //   '/api': {
    //     // 此处的写法，目的是为了 将 /api 替换成 https://www.baidu.com/
    //     target: 'https://www.baidu.com/',
    //     // 允许跨域
    //     changeOrigin: true,
    //     ws: true,
    //     pathRewrite: {
    //       '^/api': ''
    //     }
    //   },
    //   changeOrigin: true,
    // }
  },

  runtimeCompiler: true,
  lintOnSave: false
}