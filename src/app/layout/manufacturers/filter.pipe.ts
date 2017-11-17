import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter',
  pure:false,
})
export class FilterPipe implements PipeTransform {

  transform(value: any, filteredManufacturer: string ,propName : string): any {
    if(value.length===0 || filteredManufacturer===''){
      return value;
    }
    const resultArray=[];
    for(const item of value){
        if(item[propName].toLowerCase().indexOf(filteredManufacturer.toLowerCase())>=0){
          resultArray.push(item);
        }
    }
    return resultArray;
  }

}
