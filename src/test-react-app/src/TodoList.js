import React, { Component } from 'react';
import 'antd/dist/antd.css';
import { Input, Button, List } from 'antd';
import store from './store';
import { getInputChangeAction, getAddTODOItemAction, getDeleteTODOItemAction } from './store/actionCreators';

class TodoList extends Component {

    constructor(props) {
        super(props);
        this.state = store.getState();
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleStoreChagne = this.handleStoreChagne.bind(this);
        this.handleBtnClick = this.handleBtnClick.bind(this);
        this.handleItemDelete = this.handleItemDelete.bind(this);
        store.subscribe(this.handleStoreChagne);
    }

    render() {

        const { inputValue, list } = this.state;

        return (
            <div style={{marginLeft: '10px', marginTop: '10px'}}>
                <div>
                    <Input 
                        placeholder='todo info' 
                        value={inputValue} 
                        style={{width: '300px', marginRight: '10px'}}
                        onChange={this.handleInputChange}
                    />
                    <Button type='primary' onClick={this.handleBtnClick}>提交</Button>
                </div>
                <List
                    style={{width: '300px'}}
                    bordered
                    dataSource={list}
                    renderItem={(item, index) => (
                        <List.Item onClick={this.handleItemDelete.bind(this, index)}>{item}</List.Item>
                        )}
                    />
            </div>
        )
    }

    handleInputChange(e) {
        const action = getInputChangeAction(e.target.value);
        store.dispatch(action);
    }

    handleStoreChagne() {
        this.setState(store.getState());
    }

    handleBtnClick() {
        const action = getAddTODOItemAction();
        store.dispatch(action);
    }

    handleItemDelete(index) {
        const action = getDeleteTODOItemAction(index);
        store.dispatch(action);
    }
}

export default TodoList;