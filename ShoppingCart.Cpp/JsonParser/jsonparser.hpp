//
// Created by cenka on 11/25/2023.
//

#ifndef SHOPPINGCART_CPP_JSONPARSER_HPP
#define SHOPPINGCART_CPP_JSONPARSER_HPP

#include <iostream>

class jsonParser
{
public:

    jsonParser(std::string input)
    {
        _input = input;
    }

    template<typename T>
    void parseJsonToObj(std::string jsonAsString, T& obj)
    {

    }

    template<typename T>
    void parseJsonToObj(T& obj)
    {

    }

private:
    std::string _input;
    char test = 't';

};

#endif //SHOPPINGCART_CPP_JSONPARSER_HPP
