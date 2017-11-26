import * as React from "react";
import {Radio} from "react-bootstrap";

const listStyle = {
};

export class Recommendations extends React.PureComponent {
  constructor(props){
    super(props);
    this.state = {
      recommenderTypeValue: '',
    }
  }


  handleSearchInputChange = (event) => {
    this.setState({recommenderTypeValue: event.target.value});
  };

  getRecommendations = () => {
    this.props.getRecommendations(this.state.recommenderTypeValue)
  };

  render(){
    return <div style={listStyle} className="card">
      <div className="card-header">
        <h2>Recommendations</h2>
      </div>
      <div className="input-group">
        <Radio name="groupOptions" onClick={this.handleSearchInputChange} value={'cont'} >Content based</Radio>
        <Radio name="groupOptions" onClick={this.handleSearchInputChange} value={'coll'}>Collaborative filtering</Radio>
        <Radio name="groupOptions" onClick={this.handleSearchInputChange} value={'rand'}>Random</Radio>
        <span className="input-group-btn">
        <button className="btn btn-secondary" type="button" onClick={this.getRecommendations}>Go!</button>
      </span>
      </div>
      {this.props.recommendations? <ul className="list-group list-group-flush">{this.props.recommendations.map(item => <li onClick={() => this.props.showDetail(item.Id)} className="list-group-item" key={item.Id}>{item.Name}</li>)}</ul>: null}
    </div>
  }
}
