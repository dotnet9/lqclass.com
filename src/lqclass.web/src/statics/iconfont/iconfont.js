import { createGlobalStyle } from 'styled-components';

export const IconFontStyle = createGlobalStyle`
@font-face {
  font-family: 'iconfont';  /* Project id 2521370 */
  src: url('//at.alicdn.com/t/font_2521370_962259navsn.woff2?t=1620382355064') format('woff2'),
       url('//at.alicdn.com/t/font_2521370_962259navsn.woff?t=1620382355064') format('woff'),
       url('//at.alicdn.com/t/font_2521370_962259navsn.ttf?t=1620382355064') format('truetype');
}

.iconfont {
  font-family: "iconfont" !important;
  font-size: 16px;
  font-style: normal;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}
`