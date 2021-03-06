import React from 'react';
import { BrowserRouter, Route } from 'react-router-dom';

import Main from './pages/Main';

export default function Routes() {
  return (
    <BrowserRouter basename={'/teamannotation'}>
      <Route path="/" exact component={Main} />
    </BrowserRouter>
  );
}
