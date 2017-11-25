import * as React from "react";

const listStyle = {
  flexBasis: '50%',
};

export class List extends React.PureComponent {
  constructor(props){
    super(props);
    this.state = {
      searchValue: '',
    }
  }


  handleSearchInputChange = (event) => {
    this.setState({searchValue: event.target.value});
  };

  search = () => {
    this.props.search(this.state.searchValue)
  };

  render(){
    return <div style={listStyle} className="card">
      <div className="card-header">
        <h2>User rated games</h2>
      </div>
      <div className="input-group">
        <input type="text" className="form-control" placeholder="Search user by ID" aria-label="Search user by ID" onChange={this.handleSearchInputChange}></input>
      <span className="input-group-btn">
        <button className="btn btn-secondary" type="button" onClick={this.search}>Go!</button>
      </span>
      </div>
      <ul className="list-group list-group-flush">{this.props.userRating.map(item => <li onClick={() => this.props.showDetail(item.Id)} className="list-group-item" key={item.Id}>{item.Name}</li>)}</ul>
    </div>
  }
}
