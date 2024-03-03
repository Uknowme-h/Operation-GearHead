def mergeTwoLists(self, list1, list2):
    """
    :type list1: Optional[ListNode]
    :type list2: Optional[ListNode]
    :rtype: Optional[ListNode]
    """
    newlist = []
    newlist.extend(list1)
    newlist.extend(list2)
    newlist.sort()
    return newlist

list1 = [1,2,4]
list2 = [1,3,4]


