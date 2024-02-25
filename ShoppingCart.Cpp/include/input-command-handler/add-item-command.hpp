//
// Created by cenka on 2/25/2024.
//

#ifndef SHOPPINGCART_CPP_ADD_ITEM_COMMAND_HPP
#define SHOPPINGCART_CPP_ADD_ITEM_COMMAND_HPP

#include "command.hpp"

class AddItemCommand : public Command {
private:
    int itemId;
    int categoryId;
    int sellerId;
    double price;
    int quantity;

public:

    explicit AddItemCommand(int itemId, int categoryId, int sellerId, double price, int quantity)
            : itemId(itemId), categoryId(categoryId), sellerId(sellerId), price(price), quantity(quantity) {}

            void execute() override {

    }

};


#endif //SHOPPINGCART_CPP_ADD_ITEM_COMMAND_HPP
