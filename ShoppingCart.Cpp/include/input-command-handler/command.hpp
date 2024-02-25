//
// Created by cenka on 2/25/2024.
//

#ifndef SHOPPINGCART_CPP_COMMAND_HPP
#define SHOPPINGCART_CPP_COMMAND_HPP

#include <iostream>
#include <memory>
#include <unordered_map>

class Command{
public:
    virtual ~Command()= default;
    virtual void execute() = 0;
};

#endif //SHOPPINGCART_CPP_COMMAND_HPP
