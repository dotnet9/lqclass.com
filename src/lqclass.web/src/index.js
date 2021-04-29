import React, { Fragment } from 'react';
import ReactDOM from 'react-dom';
import { GlobalStyle } from './style'
import { IconFontStyle } from './statics/iconfont/iconfont'
import App from './App';

ReactDOM.render(
  <Fragment>
    <GlobalStyle />
    <IconFontStyle />
    <App />
  </Fragment>,
  document.getElementById('root')
);
