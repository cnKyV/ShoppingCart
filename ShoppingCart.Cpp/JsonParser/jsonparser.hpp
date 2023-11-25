//
// Created by cenka on 11/25/2023.
//

#ifndef SHOPPINGCART_CPP_JSONPARSER_HPP
#define SHOPPINGCART_CPP_JSONPARSER_HPP

#define OpeningBracketAscii 123
#define ClosingBracketAscii 125
#define QuoteAscii 34


#include <iostream>
#include <map>

class jsonParser
{
public:

    jsonParser(std::string input)
    {
        _input = std::move(input);
    }

    template<typename T>
    T parseJsonToObj(std::string& jsonAsString)
    {
        std::map<std::string, std::string>* deconstructedKeyValuePairs = new std::map<std::string, std::string>();

        if (jsonAsString[0] != '{')
            throw std::runtime_error("The string provided is not json.");




    }

    template<typename T>
    void parseJsonToObj(T& obj)
    {


    }

private:
    static std::string _input;

    template<typename T>
    void asciiMatcher(char& word, T& obj, std::string input = _input)
    {
        bool _isFinished = false;
        bool _isReadingOpen = false;
        size_t _indent = 0;
        size_t _index = 0;

        while(!_isFinished)
        {



        }


        if (word == OpeningBracketAscii)
        {
            _isReadingOpen = true;
            ++_index;
        }



    }




};

#endif //SHOPPINGCART_CPP_JSONPARSER_HPP
