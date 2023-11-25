//
// Created by cenka on 11/22/2023.
//

#ifndef SHOPPINGCART_CPP_INPUTRECEIVER_HPP
#define SHOPPINGCART_CPP_INPUTRECEIVER_HPP

#include <iostream>

enum class InputCommandTypeEnum
{
    AddItem,
    AddVasItemToItem,
    RemoveItem,
    ResetCart,
    DisplayCart
};

struct InputReceiver
{
    InputReceiver(std::string input)
    {

    }

};


#endif //SHOPPINGCART_CPP_INPUTRECEIVER_HPP
