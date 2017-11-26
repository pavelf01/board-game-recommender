import * as React from "react";

const detailStyle = {};

export class Detail extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      rating: 0,
    };

  }

  addRating = () => {
    this.props.addRating(this.detail.id, this.state.id);
  };

  handleRatingChange = (event) => {
    this.setState({rating: event.target.value});
  };

  render() {
    if (this.props.detail.Name) {
      return (<div style={detailStyle} className="card">
        <div className="card-body">
          <h4 className="card-title">{this.props.detail.Name}</h4>
          <p><b>Description:</b> {this.props.detail.Description}</p>
          <p><b>Categories:</b> {this.props.detail.Categories.map(category => <span>{category.Name} </span>)}</p>
          </div>
        </div>);
    }
    return null;
  }
}
