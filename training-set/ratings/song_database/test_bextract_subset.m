max_pos = 0;
max_int = 0;
pos_results = []; %Store positivity results
int_results = []; %Store intensity results
row = 1;

for b_start = 4:33
    for b_end = b_start+1:34
        % Set the columns to train on
        bextract = b_start:b_end;
        
        % Run classification
        [~, ~, accuracy_pos, accuracy_int] = classify('annotations_with_features.xlsx',bextract);

        fprintf('b_start: %i\tb_end: %i\n',b_start, b_end)
        fprintf('Positivity accuracy: %0.2f',accuracy_pos)
        if accuracy_pos > max_pos
            max_pos = accuracy_pos;
            fprintf('*')    % Just to easily tell if there was a new max
        end
        fprintf('\t')
        fprintf('Intensity accuracy: %0.2f', accuracy_int)    
        if accuracy_int > max_int
            max_int = accuracy_int;
            fprintf('*') % Just to easily tell if there was a new max
        end
        fprintf('\n');
        
        pos_results(row,:) = [b_start b_end accuracy_pos];
        int_results(row,:) = [b_start b_end accuracy_int];
        row = row + 1;
        
        pos_results = sortrows(pos_results, -3);
        int_results = sortrows(int_results, -3);
    end
end