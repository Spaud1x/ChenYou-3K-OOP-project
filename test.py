import random
class Card:
    def __init__(self,rank,suit):
        self.rank = rank
        self.suit = suit

    def getRank(self):
        self.rank = rank

    def getSuit(self):
        self.suit = suit

class Deck:
    cards = [
        'A','1','2','3','4','5','6','7','8','9','10','J','Q','K']
    suits = ['♣','♢','♡','♠']

    def __init__(self):
        self.deck = []
        for i in Deck.ranks:
            for j in Deck.suits:
                self.deck.append(i,j)

    def shuffle(self):
        random.shuffle(self.deck)

    def dealCard(self):
        return self.deck.pop(0)
