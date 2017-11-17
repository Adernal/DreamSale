import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter',
  pure:false
})
export class FilterPipe implements PipeTransform {

  transform(value: any, filteredProduct: string, propName:string,propName2:string): any {
    if(value.length===0 || filteredProduct===''){
      return value;
    }
    const resultArray=[];
    for(const item of value){
      if(item[propName].toLowerCase().indexOf(filteredProduct.toLowerCase())>=0 ||
          item[propName2].toLowerCase().indexOf(filteredProduct.toLowerCase())>=0){
        resultArray.push(item);
      }
    }
    return resultArray;
  }

}
