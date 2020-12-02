import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './Layout';
import { Calculator } from './components/Calculator/Calculator';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Calculator} />
      </Layout>
    );
  }
}
