import * as React from "react";
import {DropdownButton, MenuItem} from "react-bootstrap";

const listStyle = {
  flexBasis: '50%',
};

export class List extends React.PureComponent {
  constructor(props){
    super(props);
    this.state = {
      id: '21919',
    }
  }


  handleSearchInputChange = (event) => {
    this.setState({id: event});
  };

  search = () => {
    this.props.search(this.state.id)
  };

  render(){
    return <div style={listStyle} className="card">
      <div className="card-header">
        <h2>User rated games</h2>
      </div>
      <div className="input-group">
        <DropdownButton title={'Select user id'} onSelect={this.handleSearchInputChange} id={'select-user-id'}>
          <MenuItem eventKey="21919">21919</MenuItem>
          <MenuItem eventKey="21920">21920</MenuItem>
          <MenuItem eventKey="21921">21921</MenuItem>
          <MenuItem eventKey="21922">21922</MenuItem>
          <MenuItem eventKey="41380">41380</MenuItem>
        </DropdownButton>
      <span className="input-group-btn">
        <button className="btn btn-secondary" type="button" onClick={this.search}>{this.state.id} Go!</button>
      </span>
      </div>
      <ul className="list-group list-group-flush">{this.props.userRating.map(item => <li onClick={() => this.props.showDetail(item.BoardGame.Id)} className="list-group-item" key={item.BoardGame.Id}>
        <strong>Name:</strong> {item.BoardGame.Name} <strong>Rating:</strong> {item.Rating}
        </li>)}
        </ul>
    </div>
  }
}
