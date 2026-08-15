
#import <Foundation/Foundation.h>

extern "C" bool MacTrashFilePath(const char* filePath)
{
    NSURL *url = [NSURL fileURLWithPath:[NSString stringWithUTF8String:filePath]];
    NSError *error = nil;
    BOOL success = [[NSFileManager defaultManager]
        trashItemAtURL:url
        resultingItemURL:nil
        error:&error];
    return success;
}