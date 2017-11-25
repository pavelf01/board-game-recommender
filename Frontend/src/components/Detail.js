import * as React from "react";

const detailStyle = {

};

export class Detail extends React.Component {
  constructor(props){
    super(props);
    this.state = {
      rating: 0,
    };

  }

  addRating = () =>{
    this.props.addRating(this.detail.id, this.state.id);
  };

  handleRatingChange = (event) => {
    this.setState({rating: event.target.value});
  };

  render() {
    if(this.props.detail.Name) {
      return (<div style={detailStyle} className="card">
        <div className="card-body">
          <h4 className="card-title">{this.props.detail.Name}</h4>
          <p><b>Description:</b> {this.props.detail.Description}</p>
          <p><b>Categories:</b> {this.props.detail.Categories.map(category => <span>{category.Name} </span>)}</p>

          <div className="input-group">
            <input type="number" className="form-control" placeholder="Rating" aria-label="Add rating" min={0} max={10}
                   value={this.state.rating} onChange={this.handleRatingChange}></input>
            <span className="input-group-btn">
        <button className="btn btn-secondary" type="button">Add rating</button>
      </span>
          </div>
        </div>
      </div>);
    }
    return null;
  }
}
